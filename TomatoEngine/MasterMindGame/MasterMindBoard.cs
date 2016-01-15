using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TomatoEngine.MasterMindGame
{
    public class MasterMindBoard : GameObject
    {
        private Helpers.Grid gridAttempts = new Helpers.Grid(8,4);
        private Helpers.Grid gridOutput = new Helpers.Grid(8, 1);
        private Helpers.Grid gridAwnser = new Helpers.Grid(1, 4);
        private PawnColor[] _codeToCrack;
        private int _selectedCol = 0, _selectedRow = 0;
        private bool _gameEnded = false;
        private ScoreDBDataSetTableAdapters.ScoresTableAdapter _adapter = new ScoreDBDataSetTableAdapters.ScoresTableAdapter();
        public MasterMindBoard()
            :base()
        {
            SetStaticObject(true);
            SetTexture("mm_board");
            SetSize(12.0f,10.0f);
            Z_Index = 100;
            gridAttempts.DisableSizing = true;
            gridAttempts.SetPos(-0.90f, -1.55f);
            gridAttempts.SetSize(3.85f, 8.66f);

            gridOutput.DisableSizing = true;
            gridOutput.SetPos(4.60f, -0.7f);
            gridOutput.SetSize(1f, 8.66f);

            gridAwnser.DisableSizing = true;
            gridAwnser.SetPos(-0.90f, 9.65f);
            gridAwnser.SetSize(3.85f, 1.06f);

            Reset();
            TomatoMainEngine.AddGameObject(gridAttempts);
            TomatoMainEngine.AddGameObject(gridOutput);
            TomatoMainEngine.AddGameObject(gridAwnser);
            SelectPawn(0,0);

            MakeCodeToCrack();
            ShowAwnser();
        }

        public void Reset()
        {
            for ( int row = 0; row < 8;row++ )
            {
                for ( int col = 0; col < 4; col++ )
                {
                    gridAttempts.Add(new Pawn(PawnColor.None, PawnType.Normal), row,col);
                    gridAttempts.ObjectAt(row, col).Z_Index = 110;
                }
            }
            for ( int r = 0; r < 8;r++ )
            {
                var rowOut = new Helpers.Grid(2,2);
                rowOut.DisableSizing = true;
                gridOutput.Add(rowOut, r, 0);
                for ( int v = 0; v<2;v++ )
                {
                    for ( int h=0; h<2;h++ )
                    {
                        var p = new Pawn(PawnColor.None, PawnType.Small);
                        rowOut.Add(p, v, h);
                        p.Z_Index = 110;
                    }
                }

            }
            for ( int col = 0; col < 4; col++ )
            {
                gridAwnser.Add(new Pawn(PawnColor.White, PawnType.Normal), 0, col);
                gridAwnser.ObjectAt(0, col).Z_Index = 110;
            }
            _gameEnded = false;
        }

        public override void Update(GameSettings settings)
        {
            base.Update(settings);
            if(!_gameEnded && ControlKeys.IsKeyPressed(Keys.A)){
                SoundPool.PlaySound("woodClick");
                DeselectPawn(_selectedRow, _selectedCol);
                _selectedCol--;
                if ( _selectedCol  < 0)
                {
                    _selectedCol = 3;
                }
                SelectPawn(_selectedRow, _selectedCol);
            }
            else if (!_gameEnded &&  ControlKeys.IsKeyPressed(Keys.D) )
            {
                SoundPool.PlaySound("woodClick");
                DeselectPawn(_selectedRow, _selectedCol);
                _selectedCol++;
                if(_selectedCol > 3){
                    _selectedCol = 0;
                }
                SelectPawn(_selectedRow, _selectedCol);
            }
            if (!_gameEnded &&  ControlKeys.IsKeyPressed(Keys.W) )
            {
                SoundPool.PlaySound("woodClick");
                Pawn p = (Pawn)gridAttempts.ObjectAt(_selectedRow, _selectedCol);
                p.NextColor();
            }
            else if (!_gameEnded &&  ControlKeys.IsKeyPressed(Keys.S) )
            {
                SoundPool.PlaySound("woodClick");
                Pawn p = (Pawn)gridAttempts.ObjectAt(_selectedRow, _selectedCol);
                p.PrevColor();
            }
            if(!_gameEnded && ControlKeys.IsKeyPressed(Keys.E)){
                SubmitSet();
            }
            if (_gameEnded && ControlKeys.IsKeyPressed(Keys.R))
            {
                MakeCodeToCrack();
                Reset();
                MessageBox.Show("You won in " + (_selectedRow +1) + " steps","You won");
            }

        }

        private void MakeCodeToCrack()
        {
            _codeToCrack = new PawnColor[4] { 
                (PawnColor)TomatoMainEngine.GameRandom.Next(1, 7), 
                (PawnColor)TomatoMainEngine.GameRandom.Next(1, 7), 
                (PawnColor)TomatoMainEngine.GameRandom.Next(1, 7), 
                (PawnColor)TomatoMainEngine.GameRandom.Next(1, 7) };
        }

        private void SubmitSet()
        {
            List<PawnColor> input = new List<PawnColor>();
            List<PawnColor> codeTocrack = _codeToCrack.ToList();
            for ( int i=0; i<4;i++ )
            {
                Pawn p = (Pawn)gridAttempts.ObjectAt(_selectedRow, i);
                input.Add( p.GetPawnColor());
            }
            for ( int c = 0; c<codeTocrack.Count && c < input.Count; c++ )
            {
                if ( codeTocrack[c].Equals(input[c]) )
                {
                    AddWhite();
                    codeTocrack.RemoveAt(c);
                    input.RemoveAt(c);
                    c--;
                }
            }
            if(codeTocrack.Count != 0){
                for ( int c = 0; c<codeTocrack.Count; c++ )
                {
                    if ( isPawnColorInArray(input, codeTocrack[c]) )
                    {
                        AddRed();
                        codeTocrack.RemoveAt(c);
                        c--;
                    }
                }
            }
            else
            {
                ShowAwnser();
                LaunchTheFireWorks();
                _gameEnded = true;
                SaveScore(_selectedRow, 100);
            }
            
            DeselectPawn(_selectedRow, _selectedCol);
            _selectedRow++;
            SelectPawn(_selectedRow, _selectedCol);
        }

        private void SaveScore(int score, int time)
        {
            ScoreDBDataSet.ScoresDataTable rows = new ScoreDBDataSet.ScoresDataTable();
            _adapter.Fill(rows);
            var row = rows.NewScoresRow();
            row.Score = score;
            row.Time = time;
            row.EndEdit();
            rows.AddScoresRow(row);
            int res = _adapter.Update(rows);
            rows.AcceptChanges();
            var d = _adapter.GetData();
            
        }

        private int GetHighScore()
        {
            int outcome = 0;
            ScoreDBDataSet.ScoresDataTable rows = new ScoreDBDataSet.ScoresDataTable();
            _adapter.Fill(rows);
            foreach(ScoreDBDataSet.ScoresRow row in rows.Rows){
                if(row.Score > outcome){
                    outcome = row.Score;
                }
            }
            return outcome;
        }

        private void LaunchTheFireWorks()
        {
            for(int i = 0; i < 4; i++ ){
                TomatoMainEngine.AddGameObject(new TimedFireworks.FireWork(new PointFloat(0,-20), Pawn.ConvertPawnColor(_codeToCrack[i])));
            }
            
        }

        private void ShowAwnser()
        {
            for ( int col = 0; col < 4; col++ )
            {
                ((Pawn)gridAwnser.ObjectAt(0, col)).SetPawnColor(_codeToCrack[col]);
            }
        }

        private void AddWhite()
        {
            Helpers.Grid wrap = (Helpers.Grid)gridOutput.ObjectAt(_selectedRow,0);
            for ( int v = 1; v>=0; v-- )
            {
                for ( int h=0; h<2; h++ )
                {
                    Pawn p = (Pawn)wrap.ObjectAt(v,h);
                    if(p.GetPawnColor() == PawnColor.None){
                        p.SetPawnColor(PawnColor.White);
                        return;
                    }
                }
            }
        }
        private void AddRed()
        {
            Helpers.Grid wrap = (Helpers.Grid)gridOutput.ObjectAt(_selectedRow, 0);
            for ( int v = 1; v>=0; v-- )
            {
                for ( int h=0; h<2; h++ )
                {
                    Pawn p = (Pawn)wrap.ObjectAt(v, h);
                    if ( p.GetPawnColor() == PawnColor.None )
                    {
                        p.SetPawnColor(PawnColor.Red);
                        return;
                    }
                }
            }
        }

        private void SelectPawn(int row, int col)
        {
            Pawn p = (Pawn)gridAttempts.ObjectAt(row, col);
            p.Selected = true;
        }
        private void DeselectPawn(int row, int col)
        {
            Pawn p = (Pawn)gridAttempts.ObjectAt(row, col);
            p.Selected = false;
        }
        public override void Draw(SharpGL.OpenGL gl)
        {
            base.Draw(gl);
        }
        private bool isPawnColorInArray(List<PawnColor> array, PawnColor value)
        {
            foreach(PawnColor pc in array){
                if(pc.Equals(value)){
                    return true;
                }
            }
            return false;
        }
    }
}
