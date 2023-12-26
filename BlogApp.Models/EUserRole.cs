using System.ComponentModel;

namespace BlogApp.Models;

public enum EUserRole
{
    [Description("User")]
    User = 1,

    [Description("Editor")]
    Editor = 2,

    [Description("Administrator")]
    Administrator = 3
}
