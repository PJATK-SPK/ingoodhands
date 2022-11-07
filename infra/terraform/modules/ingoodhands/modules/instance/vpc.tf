resource "google_compute_network" "main" {
  name = format("indagoodhands-network-%s", var.environment)
}

resource "google_compute_firewall" "main" {
  name = format("indagoodhands-fw-rule-%s", var.environment)
  network = google_compute_network.main.name


  allow {
    protocol = "tcp"
    ports    = ["8080", "22"]
  }

  source_ranges = ["0.0.0.0/0"]
  target_tags = ["web"]
}
