using Doozy.Runtime.UIManager.Containers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class FeedbackPanel : MonoBehaviour
{
    [FormerlySerializedAs("Feedback")] [SerializeField] TMP_InputField feedback;

    private enum reactionEnum { None , good , neutral , bad }

    private reactionEnum _reaction;
    
    [SerializeField] private UIView feedbackPanelView;
   
    public void SubmitFeedback()//this method will be called to submit the feedback
    { 
        if (_reaction == reactionEnum.None) 
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
        FeedBackManager.Instance.SendFeedBack((int)_reaction, feedback.text, "eventid");
    }
    
    public void BadFeedbackButton() //will be called when bad feedback button is pressed
    {
        _reaction = reactionEnum.bad;
    }
    
    public void NeutralFeedbackButton()//will be called when neutral feedback button is pressed
    {
        _reaction = reactionEnum.neutral;
    }
    public void GoodFeedbackButton()//will be called when good feedback button is pressed
    {
        _reaction = reactionEnum.good; 
    }
}
