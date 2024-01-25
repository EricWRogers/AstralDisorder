using UnityEngine;
using TMPro;
using OmnicatLabs.Extensions;
using System.Collections.Generic;
using System.Linq;

public interface IUIState
{
    public void OnStateEnter(UIStateMachineController controller);
    public void OnStateUpdate(UIStateMachineController controller);
    public void OnStateFixedUpdate(UIStateMachineController controller);
    public void OnStateExit(UIStateMachineController controller);
}

public class UIStateMachineController : MonoBehaviour
{
    [Tooltip("You can choose to add a default state to start here or if left empty you will start in the Null state")]
    public UIState state;
    [Tooltip("Canvas for the HUD")]
    public Canvas canvas;
    [Tooltip("Text Area for only text based states")]
    public TextMeshProUGUI textArea;

    [HideInInspector]
    public UINullState nullState;

    private List<UIState> states = new List<UIState>();

    private void Start()
    {
        states = GetComponentsInChildren<UIState>().ToList();

        nullState = new UINullState();

        nullState = (UINullState)states.Find(state => nullState.GetType().IsAssignableFrom(state.GetType()));

        if (state == null)
        {
            state = nullState;
        }
    }

    public void ChangeState<T>() where T : IUIState
    {
        state.OnStateExit(this);

        var candidate = states.Find(state => state.GetType() == typeof(T));

        if (candidate != null)
        {
            state = candidate;
        }
        else
        {
            Debug.LogError($"Could not locate state of type {typeof(T)} in parent or children");
        }

        state.OnStateEnter(this);
    }

    public void ChangeState(UIState newState)
    {
        state.OnStateExit(this);

        var candidate = states.Find(state => state == newState);

        if (candidate != null)
        {
            state = candidate;
        }
        else
        {
            Debug.LogError($"Could not locate state of type {newState} in parent or children");
        }

        state.OnStateEnter(this);
    }

    private void Update()
    {
        Debug.Log(state.GetType());
        state.OnStateUpdate(this);
    }

    private void FixedUpdate()
    {
        state.OnStateFixedUpdate(this);
    }
}
