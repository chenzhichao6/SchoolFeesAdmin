using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace YiSha.Util
{
    /// <summary>
    /// 快速复制，并忽略NULL值
    /// </summary>
    public static class FastMapper
    {
        static Action<TS, T> CreateCopier<TS, T>()
        {
            var sTypeOf = typeof(TS);
            var tTypeOf = typeof(T);
            // 源
            var source = Expression.Parameter(sTypeOf);
            // 目标
            var target = Expression.Parameter(tTypeOf);

            // 源  所有属性
            var props1 = sTypeOf.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanRead).ToList();

            // 目标 所有属性
            var props2 = tTypeOf.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(p => p.CanWrite).ToList();

            // 共有属性
            var props = props1.Where(x => props2.Any(y => y.Name == x.Name));

            IEnumerable<Expression> assets = new List<Expression>();

            foreach (var item in props)
            {
                // 目标
                var tItem = Expression.Property(target, item.Name);
                var tType = tItem.Type;
                var tIsNullable = tType.IsGenericType && tType.GetGenericTypeDefinition() == typeof(Nullable<>);

                // 源
                var sItem = Expression.Property(source, item.Name);
                var sType = sItem.Type;
                var sIsNullable = sType.IsGenericType && sType.GetGenericTypeDefinition() == typeof(Nullable<>);
                //Debug.WriteLine(sIsNullable);

                // ===================================
                // 注释：Nullable实际是个泛型，赋值是需要转为实际类型才可赋值，否咋泛型给实际类型赋值引发异常
                // 案例：int? s = 1;int t = s; 会引发异常
                // 解决：int? s = 1;int t = Convert.ToInt32(s); 转换后解决
                // 另外：Lamnda表达式应使用 Expression.Convert(); 转换
                // 源是可为空类型
                if (sIsNullable)
                {
                    // 目标可为空
                    if (tIsNullable)
                    {
                        // 赋值表达式
                        var asset = Expression.Assign(tItem, sItem);
                        // 当源不为空的时候赋值
                        var notNull = Expression.IfThen(Expression.NotEqual(sItem, Expression.Constant(null)), asset);
                        // 加入表达式树
                        assets = assets.Append(notNull);
                    }
                    // 目标不可为空
                    else
                    {
                        // 转换源为实际类型
                        var sItemConverted = Expression.Convert(sItem, sType.GetGenericArguments().First());
                        // 赋值表达式
                        var asset = Expression.Assign(tItem, sItemConverted);
                        // 当源不为空的时候赋值
                        var notNull = Expression.IfThen(Expression.NotEqual(sItem, Expression.Constant(null)), asset);
                        // 加入表达式树
                        assets = assets.Append(notNull);
                    }
                }
                // 源不是可为空类型
                else
                {
                    // 源是否值类型
                    var sIsValueType = sType.IsValueType;
                    if (sIsValueType)
                    {
                        // 赋值表达式
                        var asset = Expression.Assign(tItem, sItem);
                        // 加入表达式树
                        assets = assets.Append(asset);
                    }
                    // 不是值类型
                    else
                    {
                        // 赋值表达式
                        var asset = Expression.Assign(tItem, sItem);
                        // 当源不为空的时候赋值
                        var notNull = Expression.IfThen(Expression.NotEqual(sItem, Expression.Constant(null)), asset);
                        // 加入表达式树
                        assets = assets.Append(notNull);
                    }
                }
            }

            // 赋值
            var tempBlock = Expression.Block(assets);
            return Expression.Lambda<Action<TS, T>>(tempBlock, source, target).Compile();
        }

        static readonly ConcurrentDictionary<string, object> Actions = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 快速的映射同名公共属性。忽略差异的字段。
        /// </summary>
        /// <typeparam name="TS">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="from">要映射的原始对象</param>
        /// <returns>映射后的新对象</returns>
        public static T MapTo<TS, T>(this TS from) where T : new()
        {
            if (from == null) return default(T);
            string name = $"{typeof(TS)}_{typeof(T)}";

            if (!Actions.TryGetValue(name, out var obj))
            {
                var ff = CreateCopier<TS, T>();
                Actions.TryAdd(name, ff);
                obj = ff;
            }
            var act = (Action<TS, T>)obj;
            T tt = new T();
            act(from, tt);

            return tt;
        }

        /// <summary>
        /// 快速的映射同名公共属性。忽略差异的字段。
        /// </summary>
        /// <typeparam name="TS">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="from">要映射的原始对象</param>
        /// <param name="to">目标对象</param>
        /// <returns>映射后的目标对象</returns>
        public static T MapTo<TS, T>(this TS from,T to) where T : new()
        {
            if (from == null) return default(T);
            string name = $"{typeof(TS)}_{typeof(T)}";

            if (!Actions.TryGetValue(name, out var obj))
            {
                var ff = CreateCopier<TS, T>();
                Actions.TryAdd(name, ff);
                obj = ff;
            }
            var act = (Action<TS, T>)obj;
            var tt = new T();
            var t = to != null ? to : tt;
            act(from, t);

            return t;
        }

        /// <summary>
        /// 快速的映射同名公共属性。忽略差异的字段。
        /// </summary>
        /// <typeparam name="TS">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="from">要映射的原始对象集合</param>
        /// <returns>映射后的对象集合</returns>
        public static List<T> MapToMany<TS, T>(this List<TS> from) where T : new()
        {
            if(from==null || !from.Any()) return new List<T>();
            var name = $"{typeof(TS)}_{typeof(T)}";

            if (!Actions.TryGetValue(name, out var obj))
            {
                var ff = CreateCopier<TS, T>();
                Actions.TryAdd(name, ff);
                obj = ff;
            }
            var act = (Action<TS, T>)obj;

            var result = new List<T>();
            foreach (var s in from)
            {
                var to = new T();
                act(s, to);
                result.Add(to);
            }

            return result;

        }

        /// <summary>
        /// 快速的映射同名公共属性。忽略差异的字段。
        /// </summary>
        /// <typeparam name="TS">源类型</typeparam>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="from">要映射的原始对象集合</param>
        /// <returns>映射后的对象集合</returns>
        public static IEnumerable<T> MapToMany<TS, T>(this IEnumerable<TS> from) where T : new()
        {
            if (from == null || !from.Any()) return new List<T>();
            var name = $"{typeof(TS)}_{typeof(T)}";

            if (!Actions.TryGetValue(name, out var obj))
            {
                var ff = CreateCopier<TS, T>();
                Actions.TryAdd(name, ff);
                obj = ff;
            }
            var act = (Action<TS, T>)obj;

            var result = new List<T>();
            foreach (var s in from)
            {
                var to = new T();
                act(s, to);
                result.Add(to);
            }

            return result;

        }
    }
}