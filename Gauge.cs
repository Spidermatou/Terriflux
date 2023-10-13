using System;
using System.Collections.Generic;
using Godot;

abstract class Gauge : IGaugeObserver
{
  private double value = 100;
  private List<IGaugeObserver> observers = new List<IGaugeObserver>();

  public void addObserver(IGaugeObserver observer){
    if (!observers.Contains(observer)) {
      observers.add(observer);
    }
  }
  public void removeObserver(int index){
    observers.removeAt(index);
  }
  public void notifyRateAll(double xVal)
  {
    foreach (IGaugeObserver go in observers){
      go.update(xVal);
    }
  }
}
