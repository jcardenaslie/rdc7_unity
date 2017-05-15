﻿using UnityEngine;

// Include this when using outside our namespace:
// using SLS.Widgets.Table;

namespace SLS.Widgets.Table {
public class Simple : MonoBehaviour {

  private Table table;

  void Start() {

    this.table = this.GetComponent<Table>();

    this.table.ResetTable();

    this.table.AddTextColumn();
    this.table.AddTextColumn();
    this.table.AddTextColumn();

    // Initialize Your Table
    this.table.Initialize(this.OnTableSelected);

    // Populate Your Rows (obviously this would be real data here)
    for(int i = 0; i < 100; i++) {
      Datum d = Datum.Body(i.ToString());
      d.elements.Add("Col1:Row" + i.ToString());
      d.elements[0].color = new Color(0, .5f, 0);
      d.elements.Add("Col2:Row" + i.ToString());
      if (i == 3)
        d.elements[1].backgroundColor = new Color(0.4f, 0.4f, 0.6f);
      d.elements.Add("Col3:Row" + i.ToString());
      this.table.data.Add(d);
    }

    // Draw Your Table
    this.table.StartRenderEngine();

  }

  // Handle the row selection however you wish (be careful as column can be null here
  //  if your table doesn't fill the full horizontal rect space and the user clicks an edge)
  private void OnTableSelected(Datum datum, Column column) {
    string cidx = "N/A";
    if(column != null)
      cidx = column.idx.ToString();
    print("You Clicked: " + datum.uid + " Column: " + cidx);
  }

}
}