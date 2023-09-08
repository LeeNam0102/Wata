namespace Wata.Commerce.Common.Objects
{
    public class RequestDtoBase
    {
        private string? _userID;

		public string? GetUserID() {
			return _userID;
		}

		public void SetUserID(string? userID)
		{
			_userID = userID;
		}
    }
}