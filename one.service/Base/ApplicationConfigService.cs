using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using one.Data;
using one.Data.Models;
using one.Data.Repository;
using System.ComponentModel.DataAnnotations;
using one.Core.Enums;




namespace one.Service
{
   public class ApplicationConfigService : ApplicationConfigRepository
    {

        public IEnumerable<OSelectListItem> GetApplicationConfig(ApplicationMetaDataCategory category)
        {

            int categoryId = (int)category;

            var m = DataContext.Pub_Metadata
                    .Where(s => s.CategoryId == categoryId)
                    .Select(s => new OSelectListItem()
                    {
                        Id = s.Id,
                        Text = s.Title
                    }).ToList();

            return m;

        }



  

    }
}
