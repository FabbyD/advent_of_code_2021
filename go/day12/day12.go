package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

type Cave string

type CaveSystem struct {
	connections map[Cave][]Cave
}

type Path struct {
	Caves []Cave
}

func main() {
	caveSystem := parseCaveSystem()
	fmt.Println(caveSystem)
	paths := findPaths(caveSystem)
	for _, path := range paths {
		fmt.Println(path.Caves)
	}
}

func findPaths(caveSystem CaveSystem) []Path {
	paths := make([]Path, 0)

	connectedCaves := caveSystem.connections["start"]
	for _, connectedCave := range connectedCaves {
		paths = findPathsRecursively(caveSystem, paths, Path{}, connectedCave)
	}

	return paths
}

func findPathsRecursively(caveSystem CaveSystem, paths []Path, currentPath Path, currentCave Cave) []Path {
	if currentCave.IsSmall() && currentPath.Contains(currentCave) {
		return paths
	}

	currentPath.Caves = append(currentPath.Caves, currentCave)

	if currentCave == "end" {
		paths = append(paths, currentPath)
		return paths
	}

	connectedCaves := caveSystem.connections[currentCave]
	for _, connectedCave := range connectedCaves {
		paths = findPathsRecursively(caveSystem, paths, currentPath, connectedCave)
	}

	return paths
}

func parseCaveSystem() CaveSystem {
	file, err := os.Open("day12_example.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	caveSystem := CaveSystem{connections: make(map[Cave][]Cave)}
	for scanner.Scan() {
		text := scanner.Text()
		tokens := strings.Split(text, "-")
		c1, c2 := Cave(tokens[0]), Cave(tokens[1])
		caveSystem.AddConnection(c1, c2)
		caveSystem.AddConnection(c2, c1)
	}

	return caveSystem
}

func (caveSystem CaveSystem) AddConnection(origin Cave, dest Cave) {
	s, ok := caveSystem.connections[origin]
	if !ok {
		s = make([]Cave, 0, 1)
	}
	s = append(s, dest)
	caveSystem.connections[origin] = s
}

func (cave Cave) IsSmall() bool {
	return strings.ToLower(string(cave)) == string(cave)
}

func (path Path) Contains(cave Cave) bool {
	for _, otherCave := range path.Caves {
		if cave == otherCave {
			return true
		}
	}

	return false
}
