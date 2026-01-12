//using Aquila.Data.Core.Storage;


//namespace Aquila.Data.Core.Query
//{
//    public static class JoinEngine
//    {
//        public static IEnumerable<Dictionary<string, object>> InnerJoin(
//            Table left,
//            Table right,
//            string leftKey,
//            string rightKey)
//        {
//            return from l in left.All()
//                   join r in right.All()
//                   on l[leftKey] equals r[rightKey]
//                   select l.Concat(r).ToDictionary(x => x.Key, x => x.Value);
//        }
//    }
//}
using Aquila.Data.Core.Storage;

namespace Aquila.Data.Core.Query
{
    public static class JoinEngine
    {
        public static IEnumerable<Dictionary<string, object>> InnerJoin(
            Table left,
            Table right,
            string leftKey,
            string rightKey)
        {
            return from l in left.All()
                   join r in right.All()
                   on l[leftKey] equals r[rightKey]
                   select MergeRows(left.Name, l, right.Name, r);
        }

        private static Dictionary<string, object> MergeRows(
            string leftTable,
            Dictionary<string, object> leftRow,
            string rightTable,
            Dictionary<string, object> rightRow)
        {
            var result = new Dictionary<string, object>();

            foreach (var kv in leftRow)
                result[$"{leftTable}.{kv.Key}"] = kv.Value;

            foreach (var kv in rightRow)
                result[$"{rightTable}.{kv.Key}"] = kv.Value;

            return result;
        }
    }
}
