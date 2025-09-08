using UnityEngine;

public class EcosystemSimulator : MonoBehaviour
{
    [Header("Estado (poblaciones)")]
    public float P = 200f; // Plantas
    public float H = 40f;  // Herbívoros
    public float C = 8f;   // Carnívoros

    [Header("Parámetros")]
    public float g  = 0.3f;   // crecimiento plantas
    public float Ch = 0.05f;  // consumo de plantas por H (del tablero)
    public float eh = 0.02f;  // eficiencia A->H
    public float mh = 0.1f;   // mortalidad H

    public float Cc = 0.02f;  // consumo de H por C (del tablero)
    public float ec = 0.01f;  // eficiencia A->C (según foto)
    public float mc = 0.1f;   // mortalidad C

    [Header("Simulación")]
    public float stepSeconds = 0.25f;
    public bool running = true;

    // Guarda los valores iniciales para reset
    float P0, H0, C0;

    void Awake() { P0 = P; H0 = H; C0 = C; }
    void OnEnable() { InvokeRepeating(nameof(Step), stepSeconds, stepSeconds); }
    void OnDisable() { CancelInvoke(nameof(Step)); }

    public void Step()
    {
        if (!running) return;

        // Guardar estado anterior para comparar
        float prevP = P, prevH = H, prevC = C;

        // Ecuaciones tal cual el tablero
        float newP = P + g * P - (Ch * H);
        float newH = H + (eh * Ch * H) - (mh * H) - (Cc * C);
        float newC = C + (ec * Cc * C) - (mc * C);

        // Clamp a 0 si baja de 0
        P = Mathf.Max(0f, newP);
        H = Mathf.Max(0f, newH);
        C = Mathf.Max(0f, newC);

        // === Mensajes en consola ===
        if (P < prevP) Debug.Log("Los herbívoros consumieron plantas");
        if (H < prevH) Debug.Log("Los carnívoros cazaron herbívoros o murieron herbívoros");
        if (C < prevC) Debug.Log("Los carnívoros disminuyeron (falta de alimento o mortalidad)");

        Debug.Log($"Estado actual -> Plantas: {Mathf.RoundToInt(P)} | Herbívoros: {Mathf.RoundToInt(H)} | Carnívoros: {Mathf.RoundToInt(C)}");
    }

    public void Pause(bool value) => running = !value;
    public void Toggle() => running = !running;

    public void ResetSim()
    {
        P = P0; H = H0; C = C0;
    }
}
