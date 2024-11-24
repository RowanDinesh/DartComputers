using Application.Web.InputModel;
using Application.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services.Interface
{
    public interface IPaginationServices<T, S> where T : class
    {
        PaginationVM<T> GetPagination(List<S> source, PaginationIM pagination);
    }
}
