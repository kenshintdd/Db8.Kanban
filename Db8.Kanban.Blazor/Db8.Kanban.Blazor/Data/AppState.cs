namespace Db8.Kanban.Blazor;

public class AppState
{
    public string Username { get; set; }
    public bool IsAuthenticated { get; set; }

    //private string _username = string.Empty;
    //private bool _isAuthenticated;

    //public event Action OnChange;

    //public bool IsAuthenticated
    //{
    //    get => _isAuthenticated;
    //    set
    //    {
    //        _isAuthenticated = value;
    //        NotifyStateChanged();
    //    }
    //}

    //public string Username
    //{
    //    get => _username;
    //    set
    //    {
    //        _username = value;
    //        NotifyStateChanged();
    //    }
    //}

    //private void NotifyStateChanged() => OnChange?.Invoke();
}
