using Unity.Netcode;

public class BingoBoardPlayerData : INetworkSerializable {

    private struct LevelPlayerData : INetworkSerializable {
        public float[] times;
        public LevelPlayerData(int playerCount) {
            times = new float[playerCount];
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
            int playerCount = 0;
            if (serializer.IsWriter) {
                serializer.SerializeValue(ref playerCount);
                if (times == null || times.Length != playerCount) {
                    times = new float[playerCount];
                }
            } else if (serializer.IsReader) {
                playerCount = times.Length;
                serializer.SerializeValue(ref playerCount);
            }

            for (int i = 0; i < playerCount; i++) {
                serializer.SerializeValue(ref times[i]);
            }
        }
    }

    private LevelPlayerData[] bingoBoardPlayerData;

    public BingoBoardPlayerData() {
        bingoBoardPlayerData = new LevelPlayerData[25];
        for (int i = 0; i < bingoBoardPlayerData.Length; i++) {
            bingoBoardPlayerData[i] = new LevelPlayerData(0);
        }
    }
    public BingoBoardPlayerData(int playerCount) {
        bingoBoardPlayerData = new LevelPlayerData[25];
        for (int i = 0; i < bingoBoardPlayerData.Length; i++) {
            bingoBoardPlayerData[i] = new LevelPlayerData(playerCount);
        }
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter {
        if (serializer.IsWriter && bingoBoardPlayerData == null) {
            bingoBoardPlayerData = new LevelPlayerData[25];
        }
        for (int i = 0; i < bingoBoardPlayerData.Length; i++) {
            bingoBoardPlayerData[i].NetworkSerialize(serializer);
        }
    }

}
