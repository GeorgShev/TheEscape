using UnityEngine;
using UnityEngine.UI;

public class TabsManager : MonoBehaviour
{
    public bool SetActiveTab;
    public int ActiveTab;
    public GameObject[] Tabs;
    public Image[] TabButtons;
    public Sprite InactiveTabBG;
    public Sprite ActiveTabBG;
    public Vector2 InactiveTabButtonSize;
    public Vector2 ActiveTabButtonSize;


    private void Awake()
    {
        foreach (GameObject gameObject in Tabs)
        {
            gameObject.SetActive(false);
        }

        if (SetActiveTab)
        {
            SwitchToTab(ActiveTab);
        }
    }


    private void OnEnable()
    {
        if (SetActiveTab)
        {
            SwitchToTab(ActiveTab);
        }
        else
        {
            foreach (GameObject gameObject in Tabs)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SwitchToTab(int TabID)
    {
        foreach(GameObject gameObject in Tabs)
        {
            gameObject.SetActive(false);
        }

        Tabs[TabID]?.SetActive(true);

        if (TabButtons.Length > 0)
        {

            foreach (Image image in TabButtons)
            {
                image.sprite = InactiveTabBG;
                image.rectTransform.sizeDelta = InactiveTabButtonSize;
            }

            TabButtons[TabID].sprite = ActiveTabBG;
            TabButtons[TabID].rectTransform.sizeDelta = ActiveTabButtonSize;
        }
    }  

   
}
