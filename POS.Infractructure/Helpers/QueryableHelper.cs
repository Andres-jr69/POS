using POS.Infractructure.Commons.Bases.Request;

namespace POS.Infractructure.Helpers
{
    public static class QueryableHelper
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queriable, BasePaginationRequest request)
        {
            return queriable.Skip((request.NumPage -1) * request.Record).Take(request.Record);
        }
    }
}
