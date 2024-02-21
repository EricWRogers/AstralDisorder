using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavingText : MonoBehaviour
{
    public Animator animator; 
    void SavingGame()
    {
        animator.SetTrigger("Saving");  
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
