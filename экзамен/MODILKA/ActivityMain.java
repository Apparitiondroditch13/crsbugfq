package com.example.creditapp;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;
import androidx.appcompat.app.AppCompatActivity;

public class MainActivity extends AppCompatActivity {
    
    private EditText etLogin, etPassword;
    private Button btnLogin;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        etLogin = findViewById(R.id.etLogin);
        etPassword = findViewById(R.id.etPassword);
        btnLogin = findViewById(R.id.btnLogin);

        btnLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                String login = etLogin.getText().toString().trim();
                String password = etPassword.getText().toString().trim();

                // Проверка на пустоту
                if (login.isEmpty() || password.isEmpty()) {
                    Toast.makeText(MainActivity.this, "Заполните все поля!", Toast.LENGTH_SHORT).show();
                    return;
                }

                // Проверка длины
                if (login.length() < 3 || password.length() < 3) {
                    Toast.makeText(MainActivity.this, "Логин и пароль должны быть не менее 3 символов!", Toast.LENGTH_SHORT).show();
                    return;
                }

                // Проверка логина и пароля
                if (login.equals("user_bob") && password.equals("pass_123")) {
                    // Успешный вход - переход на второй экран
                    Intent intent = new Intent(MainActivity.this, ClientsActivity.class);
                    startActivity(intent);
                } else {
                    Toast.makeText(MainActivity.this, "Неверный логин или пароль!", Toast.LENGTH_SHORT).show();
                }
            }
        });
    }
}