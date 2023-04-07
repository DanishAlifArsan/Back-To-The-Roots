using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<Item> inventoryItem;
    [SerializeField] private Image[] itemImage;

    // Update is called once per frame
    private void Update()
    {
        //menyembunyikan placeholder isi inventory kalau inventory kosong
        foreach (var i in itemImage)
        {
            i.gameObject.SetActive(false);   
        }

        //menampilkan gambar inventory kalau inventory ada isinya
        for (int i = 0; i < inventoryItem.Count; i++)
        {
            itemImage[i].sprite = inventoryItem[i].itemImage;   
            itemImage[i].gameObject.SetActive(true);
        }
    }

    //mengambil item dan menambahkan ke list
    public void PickUp(Item item) {
        inventoryItem.Add(item);
    }

    //memakai item dan menghapus dari list
    public void Consume(string name) {
        for (int i = 0; i < inventoryItem.Count; i++)
        {
            if (inventoryItem[i].itemName.Equals(name))
            {
                inventoryItem.Remove(inventoryItem[i]);
                break;
            }
        }
    }

    public bool isFull() {
        return inventoryItem.Count >= itemImage.Length;
    }
}
