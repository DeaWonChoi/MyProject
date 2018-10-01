using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 담당자 : 이재환
/// 수정시 간략 설명과 수정 날짜 
/// {
///   Ex : 함수명 변경 18/07/15
///     
/// }
/// </summary>

namespace RedTheSettlers.GameSystem
{
    abstract public class State
    {
        public abstract void ContinueGame(bool canLoadData);
        public abstract void Enter(StageType stageType);
        public abstract void Exit(StageType stageType);
    }

}
