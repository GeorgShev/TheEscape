using System;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI.Windows.Menu
{
    public class ConfimWindow : WindowBase
    {

        public Button Button1;

        public Action Choice1;

        public LocalizeStringEvent Button1Text;
        public LocalizeStringEvent MainTextHeading;
        public LocalizeStringEvent MainTextBody;


        public void Construct(string button1Text, string heading, string body)
        {
            Button1Text.StringReference.TableEntryReference = button1Text;
            MainTextHeading.StringReference.TableEntryReference = heading;
            MainTextBody.StringReference.TableEntryReference = body;

            RefreshText();
        }

        public void RefreshText()
        {
            Button1Text.RefreshString();
            MainTextHeading.RefreshString();
            MainTextBody.RefreshString();
        }

        private void Awake()
        {
            Button1.onClick.AddListener(Button1Click);
        }

        public void Button1Click()
        {
            Choice1?.Invoke();

            CloseWindow();
        }

        public void CloseWindow()
        {
            Destroy(gameObject);
        }

        //Have close window on parent class but not work
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
