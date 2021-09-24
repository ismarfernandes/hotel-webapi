using System;
using System.Linq;
using System.Reflection;

using Hotel.Data.Contexts;
using Hotel.Data.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace Hotel.Data.Extensions
{
    public static class ModelBuilderExtension
    {
        public static ModelBuilder ApplySeedsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var type = typeof(ISeed);
            var instances = assembly
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableTo(type))
                .Select(t => Activator.CreateInstance(t) as ISeed);

            foreach (var instance in instances)
            {
                instance.Populate(modelBuilder);
            }

            return modelBuilder;
        }
    }
}
