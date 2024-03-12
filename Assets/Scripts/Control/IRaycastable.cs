using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Control
{
    public interface IRaycastable
    {
        CursorType GetCursorType();
        bool HandleRaycast(PlayerController callingController);
    }
}

