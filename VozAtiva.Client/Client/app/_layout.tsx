import { Stack } from "expo-router";

export default function RootLayout() {
    return (
        <Stack>
            <Stack.Screen name="index" />
            <Stack.Screen name="Geolocation/latitude/longitude/latRange/longRange" />
        </Stack>
    );
}
