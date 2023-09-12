using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Account.Module.Models.User
{
    /// <summary>
    /// State of grid filters.
    /// </summary>
    public class UserGridControls : IUserFilters
    {
        /// <summary>
        /// Keep state of paging.
        /// </summary>
        public IPageHelper PageHelper { get; set; }

        public UserGridControls(IPageHelper pageHelper)
        {
            PageHelper = pageHelper;
        }

        /// <summary>
        /// Avoid multiple concurrent requests.
        /// </summary>
        public bool Loading { get; set; }

        /// <summary>
        /// Column to sort by.
        /// </summary>
        public UserFilterColumns SortColumn { get; set; }
            = UserFilterColumns.Id;

        /// <summary>
        /// True when sorting ascending, otherwise sort descending.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Column filtered text is against.
        /// </summary>
        public UserFilterColumns FilterColumn { get; set; }
            = UserFilterColumns.Id;

        /// <summary>
        /// Text to filter on.
        /// </summary>
        public string? FilterText { get; set; }


    }
}