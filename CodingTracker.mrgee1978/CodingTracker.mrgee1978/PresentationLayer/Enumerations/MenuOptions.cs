using System.ComponentModel.DataAnnotations;

namespace CodingTracker.mrgee1978.PresentationLayer.Enumerations;

// Enums for menu display
public enum MenuOptions
{
    [Display (Name = "Add coding session")]
    AddSession,

    [Display (Name = "Update coding session")]
    UpdateSession,

    [Display (Name = "Delete coding session")]
    DeleteSession,

    [Display (Name = "View coding sessions")]
    ViewSessions,

    [Display (Name = "Quit program")]
    Quit,
}
