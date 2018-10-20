using ai;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;
using FluentAssertions;

using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace test
{
    public class MakeConsoleWork : IDisposable
    {
        private readonly ITestOutputHelper _output;
        private readonly TextWriter _originalOut;
        private readonly TextWriter _textWriter;

        public MakeConsoleWork(ITestOutputHelper output)
        {
            _output = output;
            _originalOut = Console.Out;
            _textWriter = new StringWriter();
            Console.SetOut(_textWriter);
        }

        public void Dispose()
        {
            _output.WriteLine(_textWriter.ToString());
            Console.SetOut(_originalOut);
        }
    }

    public class Test : MakeConsoleWork
    {
        public Test(ITestOutputHelper output) : base(output)
        {
        }

        [Fact]
        public void Deserialize_Game_Message()
        {
            const string input = @"{""board"":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,1,0,0,0],[0,0,0,1,1,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            var obj = JsonConvert.DeserializeObject<GameMessage>(input);

            obj.maxTurnTime.Should().Be(15000);
            obj.player.Should().Be(2);
            obj.board.Length.Should().Be(8);
            obj.board[0].Length.Should().Be(8);
            obj.board[0][0].Should().Be(0);
            obj.board[3][3].Should().NotBe(0);

            // Beginning setup
            const string inputStart = @"{""board"":[[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,1,2,0,0,0],[0,0,0,2,1,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0],[0,0,0,0,0,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            var objStart = JsonConvert.DeserializeObject<GameMessage>(inputStart);

            List<KeyValuePair<int, int>> listOfMoves = AI.listOfMoves(objStart);

            listOfMoves.Count.Should().Be(4);

            const string inputEarly = @"{""board"":[[0,0,0,0,0,0,0,0],
                                                    [0,0,0,0,0,0,0,0],
                                                    [0,0,1,1,0,0,0,0],
                                                    [0,0,2,1,2,2,2,0],
                                                    [0,0,2,2,1,0,0,0],
                                                    [0,0,0,1,1,0,0,0],
                                                    [0,0,0,0,0,0,0,0],
                                                    [0,0,0,0,0,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            var objEarly = JsonConvert.DeserializeObject<GameMessage>(inputEarly);

            listOfMoves = AI.listOfMoves(objEarly);

            // for(int i = 0; i < listOfMoves.Count; i++) {
            //     Console.WriteLine(listOfMoves[i].Key.ToString() + ", " + listOfMoves[i].Value.ToString() + "\n");
            // }

            listOfMoves.Count.Should().Be(9);

            const string inputFurth = @"{""board"":[[0,0,0,0,0,0,0,0],
                                                    [0,0,0,0,0,0,0,0],
                                                    [0,0,0,0,0,0,0,0],
                                                    [0,0,0,1,1,1,2,2],
                                                    [0,0,1,1,1,1,1,0],
                                                    [2,2,1,1,1,2,1,0],
                                                    [0,0,1,1,2,2,0,0],
                                                    [0,0,1,0,2,0,0,0]],""maxTurnTime"":15000,""player"":2}";
            var objFurth = JsonConvert.DeserializeObject<GameMessage>(inputFurth);

            listOfMoves = AI.listOfMoves(objFurth);

            // for(int i = 0; i < listOfMoves.Count; i++) {
            //     Console.WriteLine(listOfMoves[i].Key.ToString() + ", " + listOfMoves[i].Value.ToString() + "\n");
            // }

            listOfMoves.Count.Should().Be(11);

            const string makeMoveBoard = @"{""board"":[[0,0,0,0,0,0,0,0],
                                                       [0,0,0,0,0,0,0,0],
                                                       [0,0,0,0,0,0,0,0],
                                                       [0,0,0,1,1,1,2,2],
                                                       [0,0,1,1,1,1,1,0],
                                                       [2,2,1,1,1,2,1,0],
                                                       [0,0,1,1,2,2,0,0],
                                                       [0,0,1,0,2,0,0,0]],""maxTurnTime"":15000,""player"":2}";

            var objMakeMove = JsonConvert.DeserializeObject<GameMessage>(makeMoveBoard);

            // MAKE THE MOVE (3, 2)
            const string newMakeMoveBoard = @"{""board"":[[0,0,0,0,0,0,0,0],
                                                          [0,0,0,0,0,0,0,0],
                                                          [0,0,0,0,0,0,0,0],
                                                          [0,0,2,2,2,2,2,2],
                                                          [0,0,1,2,1,1,1,0],
                                                          [2,2,1,1,2,2,1,0],
                                                          [0,0,1,1,2,2,0,0],
                                                          [0,0,1,0,2,0,0,0]],""maxTurnTime"":15000,""player"":2}";


            var objNewMakeMove = JsonConvert.DeserializeObject<GameMessage>(newMakeMoveBoard);
            int[][] newBoard = AI.makeMove(objMakeMove, new KeyValuePair<int, int>(3, 2));

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                   Console.Write(newBoard[i][j] + " ");
                }
                Console.Write("\n");
            }

            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    newBoard[i][j].Should().Be(objNewMakeMove.board[i][j]);
                }
            }
        }
    }
}
