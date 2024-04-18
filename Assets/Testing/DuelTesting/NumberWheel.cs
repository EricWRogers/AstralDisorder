using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumberWheel : MonoBehaviour
{
    public int Id;
    private Animator anim;
    public string itemName;
    public TextMeshProUGUI itemText;
    public Image selectedItem;
    private bool selected = false;
    public Sprite icon;

    //icon is unused and allows for an connected image to be displayed
    //itemText is similar but for text 



    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(selected)
        {
            selectedItem.sprite = icon;
            itemText.text = itemName;
        }
    }

    public void Selected()
    {
        selected = true;
    }

    public void DeSelect()
    {
        selected = false;
    }

    public void WheelSelect()
    {
        anim.SetBool("WheelSelect", true);
        itemText.text = itemName;   
    }

    public void WheelExit()
    {
        anim.SetBool("WheelSelect", false);
        itemText.text = "";
    }
}
