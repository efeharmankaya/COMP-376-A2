
using UnityEngine;
// https://www.youtube.com/watch?v=0VGosgaoTsw
public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.3f;
    public float slowdownLength = 3f;

    void Update() {
        Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;   
        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);
    }

    public void SlowMotion(){
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.2f;
    }
}
