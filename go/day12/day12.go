package main

import (
	"bufio"
	"fmt"
	"log"
	"os"
	"strings"
)

type Connection struct {
	P1 string
	P2 string
}

func main() {
	connections := readConnections()
	fmt.Println(connections)
}

func readConnections() []Connection {
	file, err := os.Open("day12_example.txt")
	if err != nil {
		log.Fatal(err)
	}
	defer file.Close()

	scanner := bufio.NewScanner(file)
	connections := make([]Connection, 0)
	for scanner.Scan() {
		text := scanner.Text()
		connections = append(connections, parseConnection(text))
	}

	return connections
}

func parseConnection(text string) Connection {
	tokens := strings.Split(text, "-")
	return Connection{tokens[0], tokens[1]}
}
