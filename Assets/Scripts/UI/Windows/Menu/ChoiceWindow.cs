using System;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace UI.Windows.Menu
{
    public class ChoiceWindow : WindowBase
    {

        public Button Button1;
        public Button Button2;

        public Action Choice1;
        public Action Choice2;
        public Action DestroyWindow;

        public LocalizeStringEvent Button1Text;
        public LocalizeStringEvent Button2Text;
        public LocalizeStringEvent MainTextHeading;
        public LocalizeStringEvent MainTextBody;


        public void Construct(string button1Text, string button2Text, string heading, string body)
        {
            Button1Text.StringReference.TableEntryReference = button1Text;
            Button2Text.StringReference.TableEntryReference = button2Text;
            MainTextHeading.StringReference.TableEntryReference = heading;
            MainTextBody.StringReference.TableEntryReference = body;
            RefreshText();
        }

        public void RefreshText()
        {
            Button1Text.RefreshString();
            Button2Text.RefreshString();
            MainTextHeading.RefreshString();
            MainTextBody.RefreshString();
        }


        private void Awake()
        {
            Button1.onClick.AddListener(Button1Click);

            Button2.onClick.AddListener(Button2Click);
        }

        public void Button1Click()
        {
            Choice1?.Invoke();

            CloseWindow();
        }

        public void Button2Click()
        {
            Choice2?.Invoke();

            CloseWindow();
        }

        public void CloseWindow()
        {
            DestroyWindow?.Invoke();

            Destroy(gameObject);

        }

        //Have close window on parent class but not work
        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
