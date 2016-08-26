using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AppCode.Util
{
    public static class ConverterExtention
    {
        /// <summary>
        /// Returns deep clone of source.
        /// Source must be Serializable.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource DeepClone<TSource>(this TSource source)
        {
            if (!(typeof(TSource).IsSerializable)) { throw new ArgumentException(string.Format("'{0}' is not Serializeable", typeof(TSource)), "source"); }

            TSource objResult;
            using (MemoryStream ms = new MemoryStream())
            {
                var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(ms, source);

                ms.Position = 0;
                objResult = (TSource)bf.Deserialize(ms);
            }
            return objResult;
        }


        private static void Transfer<TSource, TTarget>(this TSource source, TTarget target, bool clearTarget)
        {
            // All destination properties
            var targetPropertyList = typeof(TTarget).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Where(
                t =>
                t.CanWrite
                ).ToList();

            var altTarget = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Where(
                t =>
                t.CanWrite
                ).ToList();

            var sourcePropertyList = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).Where(
                t =>
                t.CanRead
                ).ToList();

            var altSource4 = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            var altSource5 = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(t => t.CanRead).ToList();

            var altSource1 = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.FlattenHierarchy).ToList();
            var altSource2 = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            var altSource3 = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(t => t.CanRead).ToList();

            var countTransfer = 0;

            foreach (PropertyInfo targetProperty in targetPropertyList)
            {
                var s1 = altSource3.FirstOrDefault(
                    t =>
                        targetProperty.Name.Equals(t.Name, StringComparison.OrdinalIgnoreCase)
                );

                if (s1 == null) { continue; }


                if(!(s1.CanRead)) { continue; }

                if(!(s1.PropertyType == targetProperty.PropertyType)) { continue; }

                // Matching source property
                var sourceProperty = altSource3.FirstOrDefault(
                    t =>
                    targetProperty.Name.Equals(t.Name)
                    &&
                    t.CanRead
                    &&
                    targetProperty.PropertyType == t.PropertyType
                    );

                if (s1 != null)
                {
                    // transfer property value (shallow clone).
                    targetProperty.SetValue(target, s1.GetValue(source, null), null); // .NET Framework 4.0 requires 3 parameters
                    countTransfer++;
                }
                else if (clearTarget)
                {
                    // clear target property.
                    Type targetType = targetProperty.PropertyType;
                    object nullValue = targetType.IsValueType ? null : Activator.CreateInstance(targetType);
                    targetProperty.SetValue(target, nullValue, null); // .NET Framework 4.0 requires 3 parameters
                    countTransfer++;
                }
                else
                {
                    // ignore target property.
                }
            }

            if (countTransfer == 0)
            {
                var t1 = targetPropertyList.Select(t => t.Name).OrderBy(t => t);
                var targetList = t1.Aggregate((current, next) => current + ", " + next);

                var t2 = sourcePropertyList.Select(t => t.Name).OrderBy(t => t);
                var sourceList = t2.Aggregate((current, next) => current + ", " + next);

                var t3 = altSource3.Select(t => t.Name).OrderBy(t => t);
                var altSourceList = t3.Aggregate((current, next) => current + ", " + next);

                throw new ApplicationException(
                    String.Format(
                        "Incompatible data types {0} {2} dont match {1} {3}"
                        , typeof(TTarget)
                        , typeof(TSource)
                        , targetList
                        , sourceList
                ));
            }
        }


        /// <summary>
        /// Transfers the content of properties from Source to Target.
        /// Target must exist.
        /// Source properties must be readable.
        /// Target properties must be writeable.
        /// Properties are matched by Name and data type.
        /// Both TSource and TTarget can be Interfaces types.
        /// Invoke like: source.Transfer(target);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Transfer<TSource, TTarget>(this TSource source, TTarget target)
        {
            Transfer(source, target, false);
        }

        /// <summary>
        /// Transfers the content of properties from Source to Target.
        /// Target must exist.
        /// Source properties must be readable.
        /// Target properties must be writeable, target properties without a matching source property are set to their default value.
        /// Properties are matched by Name and data type.
        /// Both TSource and TTarget can be Interfaces types.
        /// Invoke like: source.Transfer(target);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void TransferCleared<TSource, TTarget>(this TSource source, TTarget target)
        {
            Transfer(source, target, true);
        }



        /// <summary>
        /// Converts Source to Target by creating a new Target and transferring the properties from Source to Target.
        /// Target must have a parameterless constructor.
        /// Source properties must be readable.
        /// Target properties must be writeable.
        /// Properties are matched by Name and data type.
        /// TSource can be an Interface type.
        /// Invoke like: source.Convert(out target);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void Convert<TSource, TTarget>(this TSource source, out TTarget target) where TTarget : new()
        {
            target = new TTarget();
            Transfer(source, target, false);
        }


        /// <summary>
        /// Converts Source to Target by cloning the source, creating a new Target and transferring the properties from Source to Target.
        /// Target must have a parameterless constructor.
        /// Source must be Serializable and properties must be readable.
        /// Target properties must be writeable.
        /// Properties are matched by Name and data type.
        /// Invoke like: source.ConvertDeepClone(out target);
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void ConvertDeepClone<TSource, TTarget>(this TSource source, out TTarget target) where TTarget : new()
        {
            if (!(typeof(TSource).IsSerializable)) { throw new ArgumentException(string.Format("'{0}' is not Serializeable", typeof(TSource)), "source"); }

            target = new TTarget();
            var clone = source.DeepClone();
            Transfer(clone, target, false);
        }


    }
}


