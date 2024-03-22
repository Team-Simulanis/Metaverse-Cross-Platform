using Doozy.Runtime.Reactor;
using Doozy.Runtime.UIManager.Containers;
using Doozy.Runtime.UIManager.Listeners;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FeedbackPanel : MonoBehaviour
{
    [SerializeField] TMP_InputField Feedback;
    enum reactionEnum { None , good , neutral , bad }
    reactionEnum reaction;
    
    [SerializeField] UIView feedbackPanelView;
   
    public void SubmitFeedback()//this method will be called to submit the feedback
    { 
        if (reaction == reactionEnum.None) 
        {
            Debug.Log("please select a reation");
        }
        else
        {
            UpdateFeedBack();
            feedbackPanelView.Hide();
            Application.Quit();
        }
    }

    private void UpdateFeedBack()
    {
        FeedBackManager.Instance.SendFeedBack((int)reaction, Feedback.text, "eventid");
    }
    
    public void BadFeedbackButton() //will be called when bad feedback button is pressed
    {
        reaction = reactionEnum.bad;
    }
    
    public void NeutralFeedbackButton()//will be called when neutral feedback button is pressed
    {
        reaction = reactionEnum.neutral;
    }
    public void GoodFeedbackButton()//will be called when good feedback button is pressed
    {
        reaction = reactionEnum.good; 
    }
}
