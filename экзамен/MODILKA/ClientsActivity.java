package com.example.creditapp;

import android.os.Bundle;
import android.widget.ListView;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;
import org.json.JSONArray;
import org.json.JSONObject;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;
import java.util.ArrayList;

public class ClientsActivity extends AppCompatActivity {
    
    private ListView listView;
    private ArrayList<String> clientsList = new ArrayList<>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_clients);

        listView = findViewById(R.id.listView);

        // Загрузка данных в отдельном потоке
        new Thread(new Runnable() {
            @Override
            public void run() {
                fetchClientsFromApi();
            }
        }).start();
    }

    private void fetchClientsFromApi() {
        try {
            // URL вашего API (замените localhost на IP вашего компьютера)
            String apiUrl = "http://192.168.1.100:5000/api/clients/all"; 
            URL url = new URL(apiUrl);
            HttpURLConnection connection = (HttpURLConnection) url.openConnection();
            connection.setRequestMethod("GET");
            connection.setConnectTimeout(5000);
            connection.setReadTimeout(5000);

            int responseCode = connection.getResponseCode();
            if (responseCode == HttpURLConnection.HTTP_OK) {
                BufferedReader reader = new BufferedReader(
                    new InputStreamReader(connection.getInputStream()));
                StringBuilder response = new StringBuilder();
                String line;
                while ((line = reader.readLine()) != null) {
                    response.append(line);
                }
                reader.close();

                // Парсим JSON
                JSONArray jsonArray = new JSONArray(response.toString());
                for (int i = 0; i < jsonArray.length(); i++) {
                    JSONObject client = jsonArray.getJSONObject(i);
                    String fullName = client.getString("fullName");
                    String birthDate = client.getString("birthDate");
                    clientsList.add(fullName + " (р. " + birthDate + ")");
                }

                // Обновляем UI в главном потоке
                runOnUiThread(new Runnable() {
                    @Override
                    public void run() {
                        // Используем ArrayAdapter для отображения списка
                        android.widget.ArrayAdapter<String> adapter = 
                            new android.widget.ArrayAdapter<>(ClientsActivity.this, 
                                android.R.layout.simple_list_item_1, clientsList);
                        listView.setAdapter(adapter);
                    }
                });

            } else {
                showToast("Ошибка загрузки данных: " + responseCode);
            }
            connection.disconnect();

        } catch (Exception e) {
            e.printStackTrace();
            showToast("Ошибка: " + e.getMessage());
        }
    }

    private void showToast(final String message) {
        runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Toast.makeText(ClientsActivity.this, message, Toast.LENGTH_LONG).show();
            }
        });
    }
}