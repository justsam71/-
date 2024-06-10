using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Этот класс необходим, чтобы в AnomalyManager не создавать лишних Serializable полей и не использовать множество if-statement для каждого
 * уровня в игре. 
 */

public abstract class Anomaly : MonoBehaviour
{
    //в этот класс поля не вносить, т.к. я не смогу задать их в Unity редакторе. По той же причине методы тоже лучше здесь не прописывать.
    public virtual void StartAnomaly() {}

    public virtual void firstAnomaly() {}

    public virtual void secondAnomaly() {}
    public virtual void thirdAnomaly() {}

    public virtual void fourthAnomaly() {}

    public virtual void fifthAnomaly() {}

    public virtual void FinalAnomaly() {}

    public virtual void End() {}
}
