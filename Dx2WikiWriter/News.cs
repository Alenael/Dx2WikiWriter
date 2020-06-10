using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dx2WikiWriter
{
    public class News
    {
        public DateTime Date;
        public string Category;
        public string Title;
        public string Url;
        public string Image;
        public string InnerHtml;

        public News LoadByDataRow(DataRow row)
        {

            return this;
        }

        public Object[] GetAsDataRow()
        {
            return new Object[] { Date, Category, Title, Url, Image, InnerHtml };
        }
    }
}
