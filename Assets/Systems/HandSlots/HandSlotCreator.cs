using System.Collections.Generic;
using UnityEngine;

public class HandSlotCreator : MonoBehaviour
{
    public GameController gc;
    public List<GameObject> slots;
    public List<Texture> textures;
    public Renderer rend;
    public int shownSlots;
    public void createNewHandSlot()
    {
        if (slots.Count != 0)
        {
            shownSlots++;
            rend.material.mainTexture = textures[shownSlots - 1];
            GameObject newSlot = slots[0];
            slots[0].SetActive(true);
            gc.hand.Add(slots[0].GetComponent<CardPlacement>());
            slots.Remove(newSlot);
        }
    }
}
