import { Text, View } from "react-native";
import { useState, useEffect } from 'react';

export default function Index() {
    const [data, setData] = useState(null);

    useEffect(() => {
        fetch('http://localhost:5062/User')
            .then(res => res.json())
            .then(jsonRes => setData(jsonRes));
    }, []);

    return (
        <View
            style={{
                flex: 1,
                justifyContent: "center",
                alignItems: "center",
            }}
        >
            <Text>Abaixo está o resultado de consumir a API http://localhost:5062/User <br /> </Text>
            <Text>{JSON.stringify(data)} </Text>
        </View>
    );
}
