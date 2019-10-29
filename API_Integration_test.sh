#!/bin/bash
set -e

result=$(curl -X GET 'http://localhost:5000/api/Message' -H 'accept: application/json' --silent)
if [ "$result" == '[{"messageDateTime":"1900-01-01T00:00:00","user":"System","content":"Welcome to chat!"}]' ]; then
	echo "passed"
	exit 0
else
	echo "failed"
	exit 1
fi