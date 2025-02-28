using Services;
using UI.Windows.Menu;

namespace UI.Services.Windows
{
    public interface IWindowService : IService
    {
        void Open(WindowId windowId);

        //Need here???
        ChoiceWindow ChoiceWindow { get; set; }
        ConfimWindow ConfimWindow { get; set; }
        PauseMenu PauseMenu { get; set; }

    }
}