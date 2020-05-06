using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    public static int Vector2PopulatedCount(Vector2 v) {
        int ct = 0;
        if (!Mathf.Approximately(v.x, 0f)) {
            ct++;
        }
        if (!Mathf.Approximately(v.y, 0f)) {
            ct++;
        }

        return ct;
    }

    public static bool Vector2IsZero(Vector2 v) {
        return Vector2PopulatedCount(v) == 0;
    }

    public static bool Vector2IsFull(Vector2 v) {
        return Vector2PopulatedCount(v) == 2;
    }

    public static bool IsZero(float f) {
        return Mathf.Approximately(f, 0f);
    }
    
    public static bool NotZero(float f) {
        return !Mathf.Approximately(f, 0f);
    }
}