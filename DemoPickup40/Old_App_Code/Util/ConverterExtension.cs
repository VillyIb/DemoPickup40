using System;
using System.Linq;
using System.Reflection;
using System.IO;

namespace DemoPickup40.Util
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
            var targetPropertyList = typeof(TTarget).GetProperties().Where(
                t =>
                t.CanWrite
                );

            foreach (PropertyInfo targetProperty in targetPropertyList)
            {
                // Matching source property
                var sourceProperty = typeof(TSource).GetProperties().FirstOrDefault(
                    t =>
                    targetProperty.Name.Equals(t.Name)
                    &&
                    t.CanRead
                    &&
                    targetProperty.PropertyType == t.PropertyType
                    );

                if (sourceProperty != null)
                {
                    // transfer property value (shallow clone).
                    targetProperty.SetValue(target, sourceProperty.GetValue(source, null), null); // .NET Framework 4.0 requires 3 parameters
                }
                else if (clearTarget)
                {
                    // clear target property.
                    Type targetType = targetProperty.PropertyType;
                    object nullValue = targetType.IsValueType ? null : Activator.CreateInstance(targetType);
                    targetProperty.SetValue(target, nullValue, null); // .NET Framework 4.0 requires 3 parameters
                }
                else
                {
                    // ignore target property.
                }
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


