using Godot;
using Terriflux.Programs.Controller;
using Terriflux.Programs.Factories;
using Terriflux.Programs.GameContext;
using Terriflux.Programs.Gauges;
using Terriflux.Programs.Model.Grid;
using Terriflux.Programs.Model.Round;

namespace Terriflux.Programs.View
{
	public partial class MainScene : Node2D
	{
		private const int gameGridSize = 10;

		private readonly RoundModel roundModel = new();
		private readonly GridModel gridModel = GridFactory.CreateWasteland(gameGridSize);


		// nodes
		private RoundCounter _roundView;
		private GridView _gridView;
		private Impacts _impactsView;

		// markers
		private Marker2D _markGrid;

		private MainScene() : base() { }

		public override void _Ready()
		{
			base._Ready();

			/* ************
			 * get children
			 * */
			_markGrid = GetNode<Marker2D>("GridMark");
			_roundView = GetNode<RoundCounter>("RoundCounter");
			_impactsView = GetNode<Impacts>("Impacts");

			/* ************
			 * config grid
			 * */
			_gridView = GridFactory.CreateView(gridModel);      // create view
			_gridView.Position = _markGrid.Position;       // place it
			_gridView.Scale = new Vector2((float)0.5, (float)0.5);       // reduces size scale to be clearly visible
			gridModel.AddObserver(_gridView);        // add view as observer
			AddChild(_gridView);       // add view to scene

			/* ************
			 * config the static grid controller
			 * */
			GridController.SetGrid(gridModel);   // have to manage this new grid model
			GridController.SetRoundManager(roundModel);   // have to manage this new round model
			GridController.SetControledImpacts(_impactsView);   // have to manage this new impacts model

			/* ************
			 * config the rounds
			 * */
			RoundController.SetModel(roundModel);
			RoundController.SetView(_roundView);
		}
	}
}
