namespace UnitOfWorkPJEx_DapperRepository.Models.ViewModels
{
    public class ResultVM<T>
    {
        public int TotalRecords { get; set; }
        public List<T> Data { get; set; }

        public string[] SortColumnName { get; set; }
        public string[] SortDirection { get; set; }

        public ResultVM()
        { 
        
        }

        public void ValidateColumnName(T t) 
        {
          //  t.GetType().GetProperties();
          //  this.SortColumnName
        }

        public void ValidateSortDirection() 
        { 
        
        }
    }
}
