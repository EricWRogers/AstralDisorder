using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DefaultSelectionController : MonoBehaviour
{
    public Selectable firstDefaultSelection;

    public static DefaultSelectionController Instance;
    private EventSystem es;
    private Selectable selection;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        es = GetComponent<EventSystem>();
    }

    void Start()
    {
        OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().controlsChangedEvent.AddListener(OnDeviceChanged);
        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            firstDefaultSelection.Select();
            es.firstSelectedGameObject = firstDefaultSelection.gameObject;
        }
    }

    private void OnDeviceChanged(PlayerInput input)
    {
        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            if (selection == null)
            {
                firstDefaultSelection.Select();
                es.firstSelectedGameObject = firstDefaultSelection.gameObject;
            }
            else
            {
                selection.Select();
                es.firstSelectedGameObject = selection.gameObject;
            }
        }
        else
        {
            es.SetSelectedGameObject(null);
        }
    }

    public void ChangeSelection(Selectable uiElement)
    {
        selection = uiElement;

        if (OmnicatLabs.CharacterControllers.CharacterController.Instance.GetComponent<PlayerInput>().currentControlScheme == "Controller")
        {
            selection.Select();
            es.firstSelectedGameObject = selection.gameObject;
        }
    }
}
