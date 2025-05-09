﻿using System;
using System.IO;
using System.IO.Hashing;
using System.Reflection;
using System.Security.Cryptography;
using SteamKit2;
using SteamKit2.CDN;
using Xunit;

namespace Tests
{
    public class DepotChunkFacts
    {
        [Fact]
        public void DecryptsAndDecompressesDepotChunkPKZip()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream( "Tests.Files.depot_440_chunk_bac8e2657470b2eb70d6ddcd6c07004be8738697.bin" );
            using var ms = new MemoryStream();
            stream.CopyTo( ms );
            var chunkData = ms.ToArray();

            var chunk = new DepotManifest.ChunkData(
                id: [], // id is not needed here
                checksum: 2130218374,
                offset: 0,
                comp_length: 320,
                uncomp_length: 544
            );

            var destination = new byte[ chunk.UncompressedLength ];
            var writtenLength = DepotChunk.Process( chunk, chunkData, destination, [
                0x44, 0xCE, 0x5C, 0x52, 0x97, 0xA4, 0x15, 0xA1,
                0xA6, 0xF6, 0x9C, 0x85, 0x60, 0x37, 0xA5, 0xA2,
                0xFD, 0xD8, 0x2C, 0xD4, 0x74, 0xFA, 0x65, 0x9E,
                0xDF, 0xB4, 0xD5, 0x9B, 0x2A, 0xBC, 0x55, 0xFC
            ] );

            Assert.Equal( chunk.CompressedLength, ( uint )chunkData.Length );
            Assert.Equal( chunk.UncompressedLength, ( uint )writtenLength );

            var hash = Convert.ToHexString( SHA1.HashData( destination ) );
            Assert.Equal( "BAC8E2657470B2EB70D6DDCD6C07004BE8738697", hash );
        }

        [Fact]
        public void DecryptsAndDecompressesDepotChunkVZip()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream( "Tests.Files.depot_232250_chunk_7b8567d9b3c09295cdbf4978c32b348d8e76c750.bin" );
            using var ms = new MemoryStream();
            stream.CopyTo( ms );
            var chunkData = ms.ToArray();

            var chunk = new DepotManifest.ChunkData(
                id: [], // id is not needed here
                checksum: 2894626744,
                offset: 0,
                comp_length: 304,
                uncomp_length: 798
            );

            var destination = new byte[ chunk.UncompressedLength ];
            var writtenLength = DepotChunk.Process( chunk, chunkData, destination, [
                0xE5, 0xF6, 0xAE, 0xD5, 0x5E, 0x9E, 0xCE, 0x42,
                0x9E, 0x56, 0xB8, 0x13, 0xFB, 0xF6, 0xBF, 0xE9,
                0x24, 0xF3, 0xCF, 0x72, 0x97, 0x2F, 0xDB, 0xD0,
                0x57, 0x1F, 0xFC, 0xAD, 0x9F, 0x2F, 0x7D, 0xAA,
            ] );

            Assert.Equal( chunk.CompressedLength, ( uint )chunkData.Length );
            Assert.Equal( chunk.UncompressedLength, ( uint )writtenLength );

            var hash = Convert.ToHexString( SHA1.HashData( destination ) );
            Assert.Equal( "7B8567D9B3C09295CDBF4978C32B348D8E76C750", hash );
        }

        [Fact]
        public void DecryptsAndDecompressesDepotChunkZStd()
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream( "Tests.Files.depot_3441461_chunk_9e72678e305540630a665b93e1463bc3983eb55a.bin" );
            using var ms = new MemoryStream();
            stream.CopyTo( ms );
            var chunkData = ms.ToArray();

            var chunk = new DepotManifest.ChunkData(
                id: [], // id is not needed herein 
                checksum: 3753325726,
                offset: 0,
                comp_length: 176,
                uncomp_length: 156
            );

            var destination = new byte[ chunk.UncompressedLength ];
            var writtenLength = DepotChunk.Process( chunk, chunkData, destination, [
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10,
                0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18,
                0x19, 0x1A, 0x1B, 0x1C, 0x1D, 0x1E, 0x1F, 0x20
            ] );

            Assert.Equal( chunk.CompressedLength, ( uint )chunkData.Length );
            Assert.Equal( chunk.UncompressedLength, ( uint )writtenLength );

            var hash = Convert.ToHexString( SHA1.HashData( destination ) );
            Assert.Equal( "9E72678E305540630A665B93E1463BC3983EB55A", hash );
        }
    }
}
