using SQLite;
using SQLiteNetExtensions.Attributes;

namespace CarCare
{
	[Table("Photos")]
	public class Photo : BaseModel
	{
		private int photoID, projectID;
		private string photoPath;
		private Project projectDetail;

		[PrimaryKey, AutoIncrement]
		public int CarPhotoID
		{
			get
			{
				return photoID;
			}

			set
			{
				this.photoID = value;
				OnPropertyChanged(nameof(CarPhotoID));
			}
		}

		public string CarPhotoPath
		{
			get
			{
				return photoPath;
			}

			set
			{
				this.photoPath = value;
				OnPropertyChanged(nameof(CarPhotoPath));
			}
		}

		[ForeignKey(typeof(Project))]
        public int ProjectID
		{
			get
			{
                return projectID;
			}

			set
			{
				projectID = value;
				OnPropertyChanged(nameof(ProjectID));
			}
		}

        [ManyToOne(CascadeOperations = CascadeOperation.All)]
        public Project ProjectDetail
		{
			get
			{
                return projectDetail;
			}

			set
			{
				projectDetail = value;
				OnPropertyChanged(nameof(ProjectDetail));
			}
		}
	}
}