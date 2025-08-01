using System.Collections.Generic;
using UnityEngine;

public class HandSlotCreator : MonoBehaviour
{
    public GameController gc;
    public List<GameObject> slots;
    public List<Texture> textures;
    public Renderer rend;
    public int shownSlots;
    void Start()
    {
        rend.material.mainTexture = textures[0];
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            createNewHandSlot();
        }
    }
    public void createNewHandSlot()
    {
        if (slots.Count != 0)
        {
            shownSlots++;
            rend.material.mainTexture = textures[shownSlots];
            GameObject newSlot = slots[0];
            slots[0].SetActive(true);
            gc.hand.Add(slots[0].GetComponent<CardPlacement>());
            slots.Remove(newSlot);
        }
    }
}
