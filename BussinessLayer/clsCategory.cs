using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;

namespace BusinessLayer
{
    public class clsCategory
    {
		public enum enMode { AddNew,Update}
		private enMode _mode { set; get; }
		public long? CatID {  get; set; }

		public string Name { get; set; }
		private long PersonID { set; get; }

		public clsCategory(long PeronID)
		{
			this.PersonID = PeronID;
			this.Name = null;
			this.CatID = null;
			this._mode = enMode.AddNew;
			_DoBaseOnMode = new Dictionary<enMode, Func<Task<long?>>>();
            _DoBaseOnMode.Add(enMode.AddNew,AddNewCategory);

        }
		private Dictionary<enMode, Func<Task<long?>>> _DoBaseOnMode;
		
		public async Task<List<string>> GetGategoriesListRelatedToPersonAsync()
		{
			return await clsCategoriesData.GetCategoriesListRelatedToPersonAsync(this.PersonID);
		}
        public static async Task<List<string>> GetGategoriesListByTaskIDAsync(long TakID)
        {
            return await clsCategoriesData.GetTaskCategories(TakID);
        }
        public static async Task<DataTable> GetGategoriesRelatedToPersonAsync(long PeronID)
        {
			try
			{
				return  await clsCategoriesData.GetCategoriesRelatedToPersonAsync(PeronID);
			}
			catch (Exception e)
			{

				throw new Exception("Error retrieving categories related to the person.", e);
			}
        }
		// will return null if category exists before 
		
		
		
		
		private async Task<long?> AddNewCategory()
		{
			return await clsCategoriesData.AddNewCategoryAsync(this.PersonID,this.Name);

		}

		public async Task<bool> SaveAsync()
		{
			if(_DoBaseOnMode.ContainsKey(_mode))
			{
				if (await _DoBaseOnMode[_mode].Invoke() != null)
				{
					return true;
				}
				else
					return false;
			}

			return false;
		}


    }
}
