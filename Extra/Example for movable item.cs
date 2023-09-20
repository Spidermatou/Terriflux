public class Player : Node
{
    private IMovable selectedNode;
    private bool isDragging = false;

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == (int)ButtonList.Left)
            {
                if (mouseEvent.Pressed)
                {
                    if (selectedNode == null)
                    {
                        // Si aucun nœud n'est actuellement sélectionné
                        if (mouseEvent.Doubleclick)
                        {
                            // Double-clic pour "sélectionner" un nœud
                            selectedNode = GetMovableNodeAtPosition(mouseEvent.GlobalPosition);
                            isDragging = selectedNode != null;
                        }
                    }
                    else
                    {
                        // Si un nœud est déjà sélectionné, relâchez-le
                        selectedNode = null;
                        isDragging = false;
                    }
                }
            }
        }
        else if (isDragging && @event is InputEventMouseMotion mouseMotion)
        {
            // Si le joueur déplace la souris et qu'un nœud est sélectionné
            // Mettez à jour la position du nœud
            selectedNode.MoveTo(mouseMotion.GlobalPosition);
        }
    }

    private IMovable GetMovableNodeAtPosition(Vector2 globalPosition)
    {
        // Vous devrez implémenter cette méthode pour retourner le nœud IMovable
        // qui se trouve à la position donnée (ou null s'il n'y en a pas).
    }
}