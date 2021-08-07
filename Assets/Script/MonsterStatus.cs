﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//에디터에서 쉽게 사용할수 있도록 메뉴를 만듬
[CreateAssetMenu(fileName ="MonsterStatus",menuName="Scriptable Object Asset/MonsterStatus")]
public class MonsterStatus : ScriptableObject
{
    public int moveSpeed = 1;
    public int view = 3;//시야범위
    public bool[] random = { false, true };//왼쪽,오른쪽 의 랜덤
    public float maxTime = 3.0f;
}