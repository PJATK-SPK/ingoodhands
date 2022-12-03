resource "google_sql_database_instance" "backend" {
  name             = "backend-db"
  database_version = "POSTGRES_14"
  region           = var.region

  settings {
    # Second-generation instance tiers are based on the machine
    # type. See argument reference below.
    tier = "db-f1-micro"

    database_flags {
      name  = "cloudsql.iam_authentication"
      value = "on"
    }
  }

  lifecycle {
    prevent_destroy = true
  }
}

resource "google_sql_database" "backend" {
  provider = google-beta

  name     = "in_good_hands"
  instance = google_sql_database_instance.backend.name
}

resource "google_sql_user" "backend" {
  provider = google-beta

  name     = "backend"
  instance = google_sql_database_instance.backend.name
  password = "change_me"
}
