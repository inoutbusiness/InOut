﻿using InOut.Domain.Entities;

namespace InOut.Common
{
    public static class IDHelper
    {
        public const long INVALID_ID = -1;

        public static bool IsIdValid(this long id)
            => id > 0;

        public static bool IsIdInvalid(this long id)
            => id < 1;

        public static bool IsIdValid(this Base obj)
            => obj.Id.IsIdValid();

        public static bool IsIdInvalid(this Base obj)
            => obj.Id.IsIdInvalid();

        public static bool IsListWithItems<T>(this List<T> list)
            => list.Count > 0;
    }
}
