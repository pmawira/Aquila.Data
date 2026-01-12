using Aquila.Data.Core.Engine;

namespace Aquila.Data.Presentation.Infrastructure
{
    public static class AquilaDatabase
    {
        public static readonly DatabaseEngine Instance = new();
    }
}
