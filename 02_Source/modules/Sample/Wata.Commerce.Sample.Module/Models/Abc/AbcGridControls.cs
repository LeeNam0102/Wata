using Wata.Commerce.Common.Helpers;

namespace Wata.Commerce.Sample.Module.Models.Abc
{
    /// <summary>
    /// State of grid filters.
    /// </summary>
    public class AbcGridControls : IAbcFilters
    {
        /// <summary>
        /// Keep state of paging.
        /// </summary>
        public IPageHelper PageHelper { get; set; }

        public AbcGridControls(IPageHelper pageHelper)
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
        public AbcFilterColumns SortColumn { get; set; }
            = AbcFilterColumns.AbcID;

        /// <summary>
        /// True when sorting ascending, otherwise sort descending.
        /// </summary>
        public bool SortDescending { get; set; }

        /// <summary>
        /// Column filtered text is against.
        /// </summary>
        public AbcFilterColumns FilterColumn { get; set; }
            = AbcFilterColumns.AbcID;

        /// <summary>
        /// Text to filter on.
        /// </summary>
        public string? FilterText { get; set; }


    }
}