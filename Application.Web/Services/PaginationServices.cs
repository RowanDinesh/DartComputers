using Application.Web.InputModel;
using Application.Web.Services.Interface;
using Application.Web.ViewModel;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Web.Services
{
    public class PaginationServices<T, S> : IPaginationServices<T, S> where T : class
    {
        private readonly IMapper _mapper;

        public PaginationServices(IMapper mapper)
        {
            _mapper = mapper;
        }

        public PaginationVM<T> GetPagination(List<S> source, PaginationIM pagination)
        {
            var currentPage = pagination.PageNumber;

            var pageSize = pagination.PageSize;

            var totalNoOfRecords = source.Count;

            var totalPages = (int)Math.Ceiling(totalNoOfRecords/(double)pageSize);

            var result = source
                .Skip((pagination.PageNumber - 1)*(pagination.PageSize))
                .Take(pagination.PageSize)
                .ToList();

            var items = _mapper.Map<List<T>>(result);

            PaginationVM<T> paginationVM = new PaginationVM<T>(currentPage,totalPages,pageSize,totalNoOfRecords,items);

            return paginationVM;
        }
    }
}
