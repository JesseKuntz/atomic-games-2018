namespace ai
{
    public class GameMessage
    {
        public int maxTurnTime;
        public int player;
        public int[][] board;
        // public GameMessage(GameMessage gm) {
        //     maxTurnTime = gm.maxTurnTime;
        //     player = gm.player;
        //     board = gm.board;
        // }

        public GameMessage(int turnTime, int p, int[][] b) {
            this.maxTurnTime = turnTime;
            this.player = p;
            this.board = b;
        }
    }
}